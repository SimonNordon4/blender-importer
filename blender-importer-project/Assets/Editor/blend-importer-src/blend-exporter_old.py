import os
import sys
import bpy

print("starting blend importer")

# METHODS
@staticmethod
def resolve_arguments(argv):
    ''' Convert arguments to a dictonary '''
    settings = {}
    if "--" not in argv:  # no arguments, return nothing.
        return settings
    
    argv = argv[argv.index("--") + 1:]
    print(argv)
    for arg in argv:
        if "=" in arg:
            key, value = arg.split("=")
            settings[key] = value
        else:
            print("ERROR: find argument in non dictionary format: " + arg)
    return settings


blender249 = True
blender280 = (2,80,0) <= bpy.app.version

try:
    import Blender
except:
    blender249 = False

if not blender280:
    if blender249:
        try:
            import export_fbx
        except:
            print('error: export_fbx not found.')
            Blender.Quit()
    else :
        try:
            import io_scene_fbx.export_fbx
        except:
            print('error: io_scene_fbx.export_fbx not found.')
            # This might need to be bpy.Quit()
            raise

# Find the Blender output file

outfile = os.getenv(""UNITY_BLENDER_EXPORTER_OUTPUT_FILE"")

#export methods
@staticmethod
def init_exporter():
    if bpy.context.object.mode == 'EDIT':
        bpy.ops.object.mode_set(mode='OBJECT')
    bpy.ops.object.select_all(action='DESELECT')

@staticmethod
def remove_objects(objects):
    bpy.data.collections.remove(objects, do_unlink=True)
    return

@staticmethod
def collections_to_objects(exclude, collection_names):
    empty_dict = {{}}
    processed_collections = []
    # First create an empty for each collection
    for collection in bpy.data.collections:
        # if exclude is true, and we ignore collections in the filter.
        if exclude == True and (collection.name.lower() in collection_names) == True:
            remove_objects(collection)
            continue
        # if exclude is false (thus include) we ignore collections not in the filter.
        elif exclude == False and (collection.name.lower() in collection_names) == False:
            remove_objects(collection)
            continue
        col_obj = bpy.data.objects.new(collection.name, None)
        for obj in collection.objects:
            obj.parent = col_obj
        collection.objects.link(col_obj)
        empty_dict[collection] = col_obj
        processed_collections.append(collection)

    # Then go through and parent each collectionchild empty to the parent.
    for collection in processed_collections:
        for child in collection.children:
            child_obj = empty_dict[child]
            parent_obj = empty_dict[collection]
            child_obj.parent = parent_obj

if blender280:
    import bpy.ops
    # Export Collections
    # init_exporter()
    export_collections = {args.ExportCollections}
    if(export_collections):
        {args.ExportCollectionMethod}
    bpy.ops.export_scene.fbx(filepath=outfile,
                             check_existing=False,
                             use_selection=False,
                             use_visible={args.ExportVisible},
                             use_active_collection=False,
                             object_types={args.ExportObjects},
                             use_mesh_modifiers={args.ApplyModifiers},
                             mesh_smooth_type='OFF',
                             use_custom_props=True,
                             use_triangles={args.TriangulateMesh},
                             bake_anim={args.BakeAnimation},
                             bake_anim_use_nla_strips={args.BakeAnimationNLAStrips},
                             bake_anim_use_all_actions={args.BakeAnimationActions},
                             bake_anim_simplify_factor={args.SimplifyBakeAnimation},
                             path_mode='{args.PathMode}',
                             embed_textures={args.EmbedTextures},
                             apply_scale_options='FBX_SCALE_ALL')
elif blender249:
    mtx4_x90n = Blender.Mathutils.RotationMatrix(-90, 4, 'x')
    export_fbx.write(outfile,
                     EXP_OBS_SELECTED=False,
                     EXP_MESH=True,
                     EXP_MESH_APPLY_MOD=True,
                     EXP_MESH_HQ_NORMALS=True,
                     EXP_ARMATURE=True,
                     EXP_LAMP=True,
                     EXP_CAMERA=True,
                     EXP_EMPTY=True,
                     EXP_IMAGE_COPY=False,
                     ANIM_ENABLE=True,
                     ANIM_OPTIMIZE=False,
                     ANIM_ACTION_ALL=True,
                     GLOBAL_MATRIX=mtx4_x90n)
else:
    # blender 2.58 or newer
    import math
    from mathutils import Matrix
    # -90 degrees
    mtx4_x90n = Matrix.Rotation(-math.pi / 2.0, 4, 'X')

    class FakeOp:
        def report(self, tp, msg):
            print(""%s: %s"" % (tp, msg))

        exportObjects = ['ARMATURE', 'EMPTY', 'MESH']

        minorVersion = bpy.app.version[1];
        if minorVersion <= 58:
            # 2.58
            io_scene_fbx.export_fbx.save(FakeOp(), bpy.context, filepath=outfile,
                                         global_matrix=mtx4_x90n,
                                         use_selection=False,
                                         object_types=exportObjects,
                                         mesh_apply_modifiers=True,
                                         ANIM_ENABLE=True,
                                         ANIM_OPTIMIZE=False,
                                         ANIM_OPTIMIZE_PRECISSION=6,
                                         ANIM_ACTION_ALL=True,
                                         batch_mode='OFF',
                                         BATCH_OWN_DIR=False)
        else:
            # 2.59 and later
            kwargs = io_scene_fbx.export_fbx.defaults_unity3d()
            io_scene_fbx.export_fbx.save(FakeOp(), bpy.context, filepath=outfile, **kwargs)
        # HQ normals are not supported in the current exporter
