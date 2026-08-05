using System;
using System.Threading.Tasks;
using Godot;

public partial class SceneManager : Node {
    public static SceneManager Instance { get; private set; }

    [Export] LoadingCurtain curtain;

    bool isChangingScene;

    public override void _Ready() {
        if (Instance != null && Instance != this) {
            QueueFree();
            return;
        }
        Instance = this;
    }

    public async Task<bool> SwitchSceneAsync(string scenePath) {
        if (isChangingScene) {
            GD.PushError("Попытка повторного вызова смены сцены во время работы!");
            return false;
        }

        if (!IsScenePathValid(scenePath) || !ValidateCurtain())
            return false;

        isChangingScene = true;
        try {
            await curtain.FadeInAsync();

            var loadedScene = await LoadSceneAsync(scenePath);
            ApplyNewScene(loadedScene);

            // Ждем кадра для завершения инициализации сцены
            await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
            return true;
        }
        catch (Exception ex) {
            GD.PushError($"Сбой переключения сцены: {ex.Message}");
            return false; // Сигнализируем о сбое
        }
        finally {
            await TryFadeOutCurtainAsync();
            isChangingScene = false;
        }
    }

    private async Task<PackedScene> LoadSceneAsync(string scenePath) {
        Error err = ResourceLoader.LoadThreadedRequest(scenePath, useSubThreads: true);
        if (err != Error.Ok) {
            throw new Exception($"ResourceLoader вернул ошибку: {err}");
        }

        var progressArray = new Godot.Collections.Array();
        while (true) {
            var status = ResourceLoader.LoadThreadedGetStatus(scenePath, progressArray);

            if (status == ResourceLoader.ThreadLoadStatus.Loaded) {
                break;
            }

            if (status == ResourceLoader.ThreadLoadStatus.Failed || status == ResourceLoader.ThreadLoadStatus.InvalidResource) {
                throw new Exception($"Ошибка фоновой загрузки. Статус: {status}");
            }

            // if (progressArray.Count > 0) {
            //     var progress = (float)progressArray[0];
            //     curtain.TODO();
            // }

            // THREAD_LOAD_IN_PROGRESS
            await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        }

        return (PackedScene)ResourceLoader.LoadThreadedGet(scenePath);
    }

    private void ApplyNewScene(PackedScene scene) {
        Error changeError = GetTree().ChangeSceneToPacked(scene);
        if (changeError != Error.Ok) {
            throw new InvalidOperationException($"Не удалось сменить сцену: {changeError}");
        }
    }

    private static bool IsScenePathValid(string path) {
        if (string.IsNullOrEmpty(path) || !ResourceLoader.Exists(path)) {
            GD.PushError($"Путь '{path}' пуст или файл не существует.");
            return false;
        }
        return true;
    }

    private bool ValidateCurtain() {
        if (curtain == null) {
            GD.PushError("LoadingCurtain не назначен.");
            return false;
        }
        return true;
    }

    private async Task TryFadeOutCurtainAsync() {
        if (curtain != null && IsInstanceValid(curtain)) {
            await curtain.FadeOutAsync();
        }
    }
}
