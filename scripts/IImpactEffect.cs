public interface IImpactEffect {
  /// Возвращает true, если был задействован.
  bool Apply(HitContext context);
}
