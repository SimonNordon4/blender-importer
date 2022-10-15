import os
import sys
import bpy
import json


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

@staticmethod
def export_fbx(filepath, export_settings):
    if blender280:
        export_collections = import_settings.get("export_collections", "true").lower() == "true"
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

# get the current open blend file
blend_file = bpy.data.filepath
# get the json
json_file = blend_file + ".json"
# load json
# todo: load this into a data class.
with open(json_file) as f:
    import_settings = json.load(f)
    

