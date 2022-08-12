INCLUDE globals.ink
{beetTalk == 0: -> main | -> done}

=== main ===
Hey, you over there! Yes, you!#speaker:???#portrait:default
You must be surprised. You don't come across message boards this <color=\#FOCC44><b>dashing</b></color> everyday.#speaker:Talking Board #portrait:board_smug
I'm <i><b><color=\#621b2f>BeetJuicer</color></b></i>! The guy who made this game.#speaker:BeetJuicer #portrait:board_happy
Thank you so much for playing this little demo of mine.
Please leave a comment on this game's page and let me know what I can do to make it better!
Any feedback would be appreciated.
If you enjoyed this demo and would like to support me, please consider donating or recommending this to your friends.
Bye now! 'til we meet again!  - BeetJuicer
~ beetTalk = 1
->END

=== done ===
Well, well, well. Back to see this handsome face of mine again, are you?#speaker:BeetJuicer #portrait:board_question
So? Didja tell your friends about my game yet? #portrait:board_happy
    + [Nope.]
         Hmm... That's alright. You still have time to tell them. I'll wait. #portrait:board_neutral 
         ->END
    + [Yep!]
         Very good! Very good! That makes me a very happy man! #portrait:board_happy
~ beetTalk = 0
         ->END