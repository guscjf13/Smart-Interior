# -*- coding: utf-8 -*-
"""
Created on Tue Jan  7 12:00:03 2020

@author: Lee
"""
import math

angle = input('>')
angle = float(angle)/180*math.pi


ls = [math.cos(angle), math.sin(angle), -math.sin(angle), math.cos(angle)]
for idx, l in enumerate(ls):
    ls[idx] = round(l, 6)
    
print('%g,0,%g,0,0,1,0,0,%g,0,%g,0'%tuple(ls))