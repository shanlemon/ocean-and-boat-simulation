import time
import keyboard
import os
import sys



file_name = "test.txt"

f = open(os.path.join(sys.path[0], file_name), "r")

file_contents = f.read()

time.sleep(1)
keyboard.wait("g", suppress=True)
keyboard.write(file_contents, delay=0.05)