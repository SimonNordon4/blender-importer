import os
import sys
import bpy
import json
import enum


class ExportVisibleMode(enum.Enum):
    VISIBLE = 0
    ALL = 1


class ExportTypes(enum.IntFlag):
    Empty = 1
    Camera = 2
    Light = 4
    Armature = 8
    Mesh = 16
    Other = 32


class CollectionExportMode(enum.Enum):
    Include = 0
    Exclude = 1


class BlenderImportSettings:
    def __init__(self, json_ref):
        self.exportVisible = ExportVisibleMode(json_ref["exportVisible"])
        self.exportObjects = ExportTypes(json_ref["exportObjects"])
        self.exportCollections = json_ref["exportCollections"]
        self.collectionFilterMode = CollectionExportMode(json_ref["collectionFilterMode"])
        self.collectionNames = json_ref["collectionNames"]
        self.triangulateMesh = json_ref["triangulateMesh"]
        self.applyModifiers = json_ref["applyModifiers"]
        self.embedTextures = json_ref["embedTextures"]
        self.bakeAnimation = json_ref["bakeAnimation"]
        self.bakeAnimationNlaStrips = json_ref["bakeAnimationNlaStrips"]
        self.bakeAnimationActions = json_ref["bakeAnimationActions"]
        self.simplifyBakeAnimation = json_ref["simplifyBakeAnimation"]


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
def export_fbx(filepath, bs = None):
    if blender280:
        bpy.ops.export_scene.fbx(filepath=filepath,
                                 check_existing=False,
                                 use_selection=False,
                                 use_visible={bs.ExportVisible == ExportVisibleMode.VISIBLE},
                                 use_active_collection=False,
                                 object_types={bs.ExportObjects}, # TODO FORMAT THIS
                                 use_mesh_modifiers={bs.ApplyModifiers},
                                 mesh_smooth_type='OFF',
                                 use_custom_props=True,
                                 use_triangles={bs.TriangulateMesh},
                                 bake_anim={bs.BakeAnimation},
                                 bake_anim_use_nla_strips={bs.BakeAnimationNLAStrips},
                                 bake_anim_use_all_actions={bs.BakeAnimationActions},
                                 bake_anim_simplify_factor={bs.SimplifyBakeAnimation},
                                 path_mode='{args.PathMode}', # TODO FORMAT THIS
                                 embed_textures={bs.EmbedTextures},
                                 apply_scale_options='FBX_SCALE_ALL')

# get the current open blend file
blend_file = bpy.data.filepath
# get the json
json_file = blend_file + ".json"
# load json
with open(json_file) as f:
    json_dict = json.load(f)
    blender_settings = BlenderImportSettings(json_dict)
    
#init the export
init_exporter()

# check if we're exporting collections
if blender_settings.exportCollections:
    # if we are, we need to convert the collections to objects
    collections_to_objects(blender_settings.collectionFilterMode == CollectionExportMode.Exclude,
                           blender_settings.collectionNames)

#export the fbx back to Unity
export_fbx(blend_file + ".fbx", blender_settings)