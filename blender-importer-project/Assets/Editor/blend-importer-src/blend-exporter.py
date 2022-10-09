import os
import bpy

print("starting blend exporter")

blend_path = bpy.path.abspath("//")

# get the directory of the current file
blend_dir = os.path.dirname(blend_path)
blend_name = os.path.basename(bpy.data.filepath)

# export the contents of the file as an FBX in the same directory.
bpy.ops.export_scene.fbx(filepath=blend_dir + "\\" + blend_name + ".fbx",
                         check_existing=False,
                         use_selection=False,
                         use_active_collection=False,
                         object_types={'ARMATURE', 'CAMERA', 'LIGHT', 'MESH', 'OTHER', 'EMPTY'},
                         use_mesh_modifiers=True,
                         mesh_smooth_type='OFF',
                         use_custom_props=True,
                         bake_anim_use_nla_strips=False,
                         bake_anim_use_all_actions=False,
                         apply_scale_options='FBX_SCALE_ALL')

print("finished blend exporter")