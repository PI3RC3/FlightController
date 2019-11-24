FlightController

Demo project to illustrate use of scripts that will match transform 
position and rotation of the camera with target objects.  Common 
application would be a project where specific waypoints need to be 
set (that result in specific viewing angles on arrival at waypoint)
while the user walks through an environment.

Movement to various waypoints can be done via buttons or by clicking 
on the waypoint objects themselves.

Matching final rotation was problematic using Quaternion.Lerp.  The 
need to determine a differential by comparing the two objects position
creates a Vector.Zero for the look direction.  This was overcome by 
using a 'ghost' object.
