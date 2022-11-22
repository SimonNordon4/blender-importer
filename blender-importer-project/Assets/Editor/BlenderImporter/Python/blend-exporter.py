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
        self.simplifyBakeAnimation = json_ref["SimplifyBakeAnimation"]


# #export methods
# @staticmethod
# def init_exporter():
#     if bpy.context.object.mode == 'EDIT':
#         bpy.ops.object.mode_set(mode='OBJECT')
#     bpy.ops.object.select_all(action='DESELECT')

@staticmethod
def remove_objects(objects):
    bpy.data.collections.remove(objects, do_unlink=True)
    return

@staticmethod
def collections_to_objects(exclude, collection_names):
    empty_dict = {}
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
def export_fbx(file_path, bs = None):
    blender280 = (2,80,0) <= bpy.app.version
    if blender280:
        print("OBJECT TYPES: ", bs.exportObjects)
        print("FILEPATH: ", file_path)
        use_visible = bs.exportVisible == ExportVisibleMode.VISIBLE
        path_mode = 'COPY' if bs.embedTextures else 'AUTO'
        
        # create a set of string fro bs.exportObjects
        export_types = set()
        if bs.exportObjects & ExportTypes.Empty:
            export_types.add('EMPTY')
        if bs.exportObjects & ExportTypes.Camera:
            export_types.add('CAMERA')
        if bs.exportObjects & ExportTypes.Light:
            export_types.add('LIGHT')
        if bs.exportObjects & ExportTypes.Armature:
            export_types.add('ARMATURE')
        if bs.exportObjects & ExportTypes.Mesh:
            export_types.add('MESH')
        if bs.exportObjects & ExportTypes.Other:
            export_types.add('OTHER')
        
        bpy.ops.export_scene.fbx(filepath=file_path,
                                 check_existing=False,
                                 use_selection=False,
                                 use_visible=use_visible,
                                 use_active_collection=False,
                                 object_types=export_types,
                                 use_mesh_modifiers=bs.applyModifiers,
                                 mesh_smooth_type='OFF',
                                 use_custom_props=True,
                                 use_triangles=bs.triangulateMesh,
                                 bake_anim=bs.bakeAnimation,
                                 bake_anim_use_nla_strips=bs.bakeAnimationNlaStrips,
                                 bake_anim_use_all_actions=bs.bakeAnimationActions,
                                 bake_anim_simplify_factor=bs.simplifyBakeAnimation,
                                 path_mode=path_mode,
                                 embed_textures=bs.embedTextures,
                                 apply_scale_options='FBX_SCALE_ALL')

# get the current open blend file
blend_file = bpy.data.filepath
# get the json
json_file = blend_file + ".json"
# load json
with open(json_file) as f:
    json_dict = json.load(f)
    blender_settings = BlenderImportSettings(json_dict)
    
# check if we're exporting collections
if blender_settings.exportCollections:
    # if we are, we need to convert the collections to objects
    filter_mode = blender_settings.collectionFilterMode == CollectionExportMode.Exclude
    print("FILTER MODE: ", filter_mode)
    collections_to_objects(filter_mode, blender_settings.collectionNames)
    
# apply all transforms
bpy.ops.object.transform_apply(location=True, rotation=True, scale=True)

#export the fbx back to Unity
export_fbx(blend_file + ".fbx", blender_settings)