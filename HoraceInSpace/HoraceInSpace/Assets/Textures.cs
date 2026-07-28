using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Assets;

public static class Textures
{
    public static Texture2D Horace { get; private set; }
    public static Vector2 HoraceOrigin => new(Horace.Width/2f, Horace.Height/2f);
    
    public static Texture2D AsteroidSmall { get; private set; }
    public static Texture2D ShadowOverlaySmall { get; private set; }
    public static Vector2 AsteroidSmallOrigin => new(AsteroidSmall.Width/2f, AsteroidSmall.Height/2f);
    
    public static Texture2D AsteroidMedium { get; private set; }
    public static Texture2D ShadowOverlayMedium { get; private set; }
    public static Vector2 AsteroidMediumOrigin => new(AsteroidMedium.Width/2f, AsteroidMedium.Height/2f);
    
    public static Texture2D AsteroidBig { get; private set; }
    public static Texture2D ShadowOverlayBig { get; private set; }
    public static Vector2 AsteroidBigOrigin => new(AsteroidBig.Width/2f, AsteroidBig.Height/2f);
    
    public static Texture2D AsteroidGiant { get; private set; }
    public static Texture2D ShadowOverlayGiant { get; private set; }
    public static Vector2 AsteroidGiantOrigin => new(AsteroidGiant.Width/2f, AsteroidGiant.Height/2f);
    
    public static Texture2D AsteroidEnormous { get; private set; }
    public static Texture2D ShadowOverlayEnormous { get; private set; }
    public static Vector2 AsteroidEnormousOrigin => new(AsteroidEnormous.Width/2f, AsteroidEnormous.Height/2f);
    
    public static Texture2D Ufo { get; private set; }
    public static Vector2 UfoOrigin => new(Ufo.Width/2f, Ufo.Height/2f);
    
    public static Texture2D Bullet { get; private set; }
    public static Vector2 BulletOrigin => new(Bullet.Width/2f, Bullet.Height/2f);
    
    public static Texture2D Thruster { get; private set; }
    public static Vector2 ThrusterOrigin => new(Thruster.Width/2f, Thruster.Height/2f);
    
    
    public static Texture2D Pixel;

    public static void Initialize(GraphicsDevice graphicsDevice){
        Pixel = new Texture2D(graphicsDevice, 1, 1);
        Pixel.SetData([Color.LightCyan]);

    }

    public static void LoadContent(ContentManager content)
    {
        Horace = content.Load<Texture2D>("horace");
        
        AsteroidSmall = content.Load<Texture2D>("asteroid_small");
        ShadowOverlaySmall = content.Load<Texture2D>("shadow_overlay_small");
        
        AsteroidMedium = content.Load<Texture2D>("asteroid_medium");
        ShadowOverlayMedium = content.Load<Texture2D>("shadow_overlay_medium");
        
        AsteroidBig = content.Load<Texture2D>("asteroid_big");
        ShadowOverlayBig = content.Load<Texture2D>("shadow_overlay_big");
        
        AsteroidGiant = content.Load<Texture2D>("asteroid_giant");
        ShadowOverlayGiant = content.Load<Texture2D>("shadow_overlay_giant");
        
        AsteroidEnormous = content.Load<Texture2D>("asteroid_enormous");
        ShadowOverlayEnormous = content.Load<Texture2D>("shadow_overlay_enormous");
        
        Ufo = content.Load<Texture2D>("ufo");
        Bullet = content.Load<Texture2D>("bullet");
        Thruster = content.Load<Texture2D>("thruster");
    }
}