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