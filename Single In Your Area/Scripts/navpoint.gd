extends Sprite



var alpha = 1

var startTime

var fadeSpeed = 0.05



func _ready():

    startTime = OS.get_unix_time()

    

func _process(delta):

    # Called every frame. Delta is time since last frame.

    # Update game logic here.

    if(OS.get_unix_time()-startTime > 0.8):

        self.modulate.a = alpha

        alpha -= fadeSpeed

    if (Input.is_action_pressed("left_click")):



        alpha = 1

        self.modulate.a = alpha

        startTime = OS.get_unix_time()
