#DialogBox.gd

extends RichTextLabel

var dialog = ["Hey! Welcome to the game!!", "Explore the area and watch your anxiety fly~", 
"Where is your soulmate?", "They don't seem to be in this area.", "Are they somewhere else?",
"Are they real?", "Or are you alone in this world after all?", "Only one way to find out." ]
var page = 0


func _ready():
	set_bbcode(dialog[page])
	set_visible_characters(0)
	set_process_input(true)
	
	
func _input(event):
	if (Input.is_action_pressed("left_click")):
		if get_visible_characters() > get_total_character_count():
			if page < dialog.size()-1:
				page += 1
				set_bbcode(dialog[page])
				set_visible_characters(0)
				
		else:
			set_visible_characters(get_total_character_count())

	

#func _process(delta):
#	# Called every frame. Delta is time since last frame.
#	# Update game logic here.
#	pass


func _on_Timer_timeout():
	set_visible_characters(get_visible_characters()+1)