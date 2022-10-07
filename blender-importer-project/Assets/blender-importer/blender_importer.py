import os
import sys
import bpy

print("starting blend importer")

# METHODS
@staticmethod
def resolve_arguments(argv):
    ''' Convert arguments to a dictonary '''
    if "--" not in argv:  # no arguments, add defaults.
        return {
            "path": "E:\\repos\\blender-to-unity\\blender-to-unity\\Assets\\01-scripts\\blender-importer",
            "name": "default_export",
        }
    argv = argv[argv.index("--") + 1:]
    settings = {}
    print(argv)
    for arg in argv:
        if "=" in arg:
            key, value = arg.split("=")
            settings[key] = value
        else:
            print("ERROR: find argument in non dictionary format: " + arg)
    return settings

print("HELLO")
import_settings = resolve_arguments(sys.argv)

blend_path = import_settings["blend_path"]
blend_name = import_settings["blend_name"]
import_collections = import_settings["import_collections"]
import_materials = import_settings["import_materials"]
bake_shaders = import_settings["bake_shaders"]



# print("EXPORTING SCENE TO " + blend_path +  blend_name + ".fbx")

# dir = blend_path +  blend_name + ".fbx"

# bpy.ops.export_scene.fbx(filepath=dir,
#         check_existing=False,
#         use_selection=False,
#         use_active_collection=False,
#         object_types= {'ARMATURE','CAMERA','LIGHT','MESH','OTHER','EMPTY'},
#         use_mesh_modifiers=True,
#         mesh_smooth_type='OFF',
#         use_custom_props=True,
#         bake_anim_use_nla_strips=False,
#         bake_anim_use_all_actions=False,
#         apply_scale_options='FBX_SCALE_ALL')