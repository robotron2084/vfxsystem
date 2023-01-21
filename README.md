# FX System Package
FX System is a way to bridge animations and scripts, allowing easy spawning of effects, and other game objects right from animators.
# How to add to your project
### 2019.3 or higher
`Window > Package Manager > + (top left button) > add package from git URL...` 

paste `https://gitlab.com/ASeward/gamejamstarterkit.git#fx_system`

### 2019.2 or less
In your projects root directory (where the assets folder is) open `Packages > manifest.json`

**remove the core package line if you've already installed the core package.**

add this under `dependencies {`

```
"com.aseward.game-jam.starter-kit.core": "git+https://gitlab.com/ASeward/gamejamstarterkit.git#core",
"com.aseward.game-jam-starter-kit.fxsystem": "git+https://gitlab.com/ASeward/gamejamstarterkit.git#fx_system"
```

so your manifest.json should look roughly like

```json5
{
     "dependencies": {
        "com.aseward.game-jam.starter-kit.core": "git+https://gitlab.com/ASeward/gamejamstarterkit.git#core",
        "com.aseward.game-jam-starter-kit.fxsystem": "git+https://gitlab.com/ASeward/gamejamstarterkit.git#fx_system",
        /// other dependencies
     } 
}
```