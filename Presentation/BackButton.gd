extends Node2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
    get_tree().set_auto_accept_quit(false)
    pass # Replace with function body.


func _notification(what):
    if (what == MainLoop.NOTIFICATION_WM_GO_BACK_REQUEST):
        get_tree().change_scene("res://Presentation/MainMenu/MainMenu.tscn")
    pass
