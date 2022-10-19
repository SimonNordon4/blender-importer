import os
import sys
from dataclasses import dataclass
from typing import List
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


json_file = r"E:\repos\blender-importer\blender-importer-project\Assets\default_cube.blend.json"
# load json

with open(json_file) as f:
    json_dict = json.load(f)
    blender_settings = BlenderImportsettings(json_dict)

print(blender_settings.exportObjects)