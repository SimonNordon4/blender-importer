import os
import sys
import bpy

print("starting blend importer")

# METHODS
@staticmethod
def resolve_arguments(argv):
    ''' Convert arguments to a dictonary '''
    settings = {}
    if "--" not in argv:  # no arguments, add defaults.
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

print("HELLO")
import_settings = resolve_arguments(sys.argv)





