INCLUDE globals.ink

Hey, hey hey! #speaker:Dr. Green #portrait:dr_green_neutral #layout:left

{ drGreenIntroduced == false: -> main | -> already_introduced }

=== main === 
How are you?
+ [Fine.]
    That's good! #portrait:dr_green_happy
+ [I'm not feeling too good.]
    Is that so?  #portrait:dr_green_sad
    Well, no worries, I'll fix you right up! #portrait:dr_green_happy
    
- Ey, whassup doc? You need anything? #speaker:Rhastaman #portrait:rhastaman_neutral #layout:right
- Hey, Rhastaman. No, I don't need anything right now. #speaker:Dr. Green #portrait:dr_green_happy #layout:left
- Rhastaman's a pretty cool guy, you should go talk to him if you haven't already.
- He might even give you a pokemon.

Well, do you have any other concerns? #speaker:Dr. Green #portrait:dr_green_neutral #layout:left
~ drGreenIntroduced = true
+ [Yes]
    -> main
+ [No]
    Goodbye then!
    -> END
    
=== already_introduced ===
Need anything? #speaker:Dr. Green #portrait:dr_green_neutral #layout:left
+ [No, it's nothing.]
    Okay, then. Tell me if you need to get fixed up.
    ->END
+ [I need healing.]
    Alright, this won't take long.
    ->END