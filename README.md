# S52-HPGL-Editor

Simple vector HPGL editor.
![Screanshot](https://github.com/pavelpasha/S52-HPGL-Editor/blob/master/HPGL_Editor_screanshot.jpg?raw=true)

# How does it work:
Open a S52 symbol (it can only open one symbol, not a whole Library). Example symbols are presented in "symbols" directory.

The are 3 editing mode:
1) Edit whole symbol (default mode): you can move a symbol pivot point with mumericUpDown controls and also move whole symbol with a keyboard arrow keys.
2) Edit geometry: This mode will be activated when you chose a certain geometry from geometry explorer list. In this mode you can move, rotate and scale selected geometry. 
3) Edit geometry points: In order to active this mode - select geometry and then pick one of its points with mouse. When point is selected it can be moved (with keyboard arrows or direct coordinates assigment) and deleted ("delete" key). To add new points to selected goemetry - chose "Add points" from "Edit mode" menu. 

# ViewPort controls:
Zoom: mouse wheel
Pan: mouse middle button + mouse move
