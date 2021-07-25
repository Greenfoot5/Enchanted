
/// <summary>
/// <para>The interface to store projectile data.</para>
/// The class must inherit this interface in order to be able to shoot travelling projectiles.
/// </summary>
public interface IProjectileData
{
    /// <summary>
    /// The amount of time that the projectile will last.
    /// </summary>
    public float LifeTime { get; }

    /// <summary>
    /// The speed of which the projectile travels.
    /// </summary>
    public float Speed { get; }
}
