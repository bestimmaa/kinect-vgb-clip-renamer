#!/usr/bin/python

import sys
from xml.dom import minidom
import os
from operator import add
import ntpath

# build two filenames from first argument and check if they exist

working_dir = sys.argv[1]
old_name = sys.argv[2]
new_name = sys.argv[3]

print ("Looking for {}.xef/.vgclip for renaming to {}.xef/.vgclip ".format(old_name,new_name))

directory, filename = ntpath.split(working_dir)
print ("Searching in {}".format(directory))

filename_vgbclip = old_name + ".vgbclip"
filename_xef = old_name + ".xef"

both_files_exist = ntpath.exists(directory+"\\"+filename_xef) and ntpath.exists(directory+"\\"+filename_vgbclip)

print both_files_exist

# if they do

# rename the .xef 

# open the .vgbclip

# search for .xef filepath in xml

# replace filepath with new filename

# rename the .cvgclip