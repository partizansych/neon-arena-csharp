using Godot;

public static class NodeExtensions {
  public static T GetComponent<T>(this Node node) where T : class {
    if (node is T selfComponent) return selfComponent;

    var childCount = node.GetChildCount();
    for (int i = 0; i < childCount; i++) {
      var child = node.GetChild(i);
      if (child is T component) return component;
    }
    return null;
  }

  public static bool TryGetComponent<T>(this Node node, out T component) where T : class {
    if (node is T selfComponent) {
      component = selfComponent;
      return true;
    }

    var childCount = node.GetChildCount();
    for (int i = 0; i < childCount; i++) {
      var child = node.GetChild(i);
      if (child is T foundComponent) {
        component = foundComponent;
        return true;
      }
    }
    component = null;
    return false;
  }
}
