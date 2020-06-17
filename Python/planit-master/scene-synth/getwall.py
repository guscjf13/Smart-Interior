# -*- coding: utf-8 -*-
"""
Created on Sun Jan  5 14:54:09 2020

@author: Lee
"""
#            "min": [
#              37.813281747259246,
#              -8.881783998477905e-18,
#              37.38867563535251
#            ],
#            "max": [
#              43.5701650417559,
#              2.739999938756229,
#              42.62508686699518
#            ]

import json

str='{'
while True:
    input_str = input(">")
    if input_str == "":
        break
    else:
        str += input_str.rstrip('\n')
str += '}'
dict = json.loads(str)


minX = round(dict['min'][0],2)
minY = round(dict['min'][2],2)
maxX = round(dict['max'][0],2)
maxY = round(dict['max'][2],2)

print('[{0}, 0, {1}], [{2}, 0, {3}]\n'.format(minX, minY, minX, maxY))
print('[{0}, 0, {1}], [{2}, 0, {3}]\n'.format(maxX, maxY, maxX, minY))
print('[{0}, 0, {1}], [{2}, 0, {3}]\n'.format(maxX, minY, minX, minY))
print('[{0}, 0, {1}], [{2}, 0, {3}]\n'.format(minX, maxY, maxX, maxY))

