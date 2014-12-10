#!/usr/bin/python

import sys
from xml.dom import minidom
import os
from operator import add
import ntpath
import shutil

# build two filenames from first argument and check if they exist

working_dir = sys.argv[1]
old_name = sys.argv[2]
new_name = sys.argv[3]

print ("Looking for {}.xef/.vgclip for renaming to {}.xef/.vgclip ".format(old_name,new_name))

directory, filename = ntpath.split(working_dir)
print ("Searching in {}".format(directory))

filename_vgbclip = old_name + ".vgbclip"
filename_xef = old_name + ".xef"

path_vgbclip = directory+"\\"+filename_vgbclip;
path_xef= directory+"\\"+filename_xef

both_files_exist = ntpath.exists(path_vgbclip) and ntpath.exists(path_xef)

print both_files_exist

# if they do

if both_files_exist:
	print "yey"
	print path_vgbclip
	# open the .vgbclip
	xmldoc = minidom.parse(path_vgbclip)
	# search for .xef filepath in xml
	clip_element = xmldoc.getElementsByTagName("Clip")
	node_clip = clip_element[0].firstChild
	filename_xef_in_vgblip =  node_clip.nodeValue
	if filename_xef_in_vgblip in filename_xef:
		print "Smooth.."
		# replace filepath with new filename
		node_clip.replaceWholeText(new_name+".xef")
		filename_vgbclip_new = new_name + ".vgbclip"
		path_vgbclip_new = directory + "\\" + filename_vgbclip_new
		file_handle = open(path_vgbclip_new, "wb")
		xmldoc.writexml(file_handle)
		file_handle.close()
		print ".vgbclip written"
		filename_xef_new = new_name + ".xef"
		path_xef_new = directory + "\\" + filename_xef_new
		shutil.move(path_xef, path_xef_new)
else:
	print "nay"

# rename the .xef 




