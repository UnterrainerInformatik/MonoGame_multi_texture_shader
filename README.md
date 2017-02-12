# MonoGame_multi_texture_shader
A test-repo to showcase a strange behavior that may be a bug.



### Notes

The repo should contain all the files necessary for compilation.

I've tried this using the latest develop NuGet available (3.6.0.1518)

The project is WindowsDX as is my game that I've encountered this behavior on.

Maybe I've done all the shaders wrong with the exception of the 'new' shader which obviously works.

### Update

I've updated the repo with a few bug-fixes and added another series of tests that contain an additive shader that fetches values from the textures in the 'normal' order (base-texture, then displacement-texture) since the theory was that that order matters somehow when MG assigns the textures to the samplers.

Seems to make a difference when actually it really shouldn't...

![results](https://github.com/UnterrainerInformatik/MonoGame_multi_texture_shader/blob/master/results/psilo1.png)

#### Row1 and 2

The shader for those rows is a displacement shader. I take a value from the second map and use this value to look up the right, displaced, value in the base-map.

Therefore the fetching order here is 2 -> 1.

(The first column actually shows the successful output of the displacement shader. All columns should look like that.)

#### Row3

The shader for this row is a additive shader. I take a value from the first and the second map, divide them by two, add them and return that as the resulting color.

Therefore the fetching order here is 1 -> 2.

(The first (new), the third (mixed) and fifth (tex2D) columns actually show the successful output of the additive shader. All columns should look like that.)

### Open Questions

* Why does my version of the 'set second texture directly through registers' not work. There has to be a coding mistake somewhere. I see it far to often in examples. I'm pretty sure that it worked sometime.
* Why does the tex2D-way not work with the displacement shader, but with the additive shader?
* Why does the mixed-way not work with the displacement shader, but with the additive shader?
* Mix-switched column:
  While it is obvious why the 'mixed_switched' shader code wouldn't work with the additive shader, why is it displaying something with the displacement shader at all.
  From the looks of it it's displacing the base-texture with the base-texture.