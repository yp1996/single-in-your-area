extends Container

# class member variables go here, for example:
# var a = 2
# var b = "textvar"

func _ready():
	# Called every time the node is added to the scene.
	# Initialization here
	pass

#func _process(delta):
#	# Called every frame. Delta is time since last frame.
#	# Update game logic here.
#	pass
func _change_page(pageName):
	print("pageName")
	for N in self.get_children():
		if(N.get_name() == pageName):
			N.show()
			print("["+N.get_name()+"] matches")
		else:
			N.hide()
			print("["+N.get_name()+"] not match")
	# Called every time the node is added to the scene.
	# Initialization here
	pass


func _on_NextButton_pressed():
	pass # replace with function body
