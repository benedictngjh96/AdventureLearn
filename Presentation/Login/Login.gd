extends Node
var google = load("res://BusinessLogic/Google.gd").new()
var login = load("res://BusinessLogic/LoginBL.cs").new()
var global = preload("res://Global/Global.cs");

func _ready():
	pass

func _on_LoginBtn_pressed():
	google.google_connect()
	#login.InsertUser()
	global.SetStudentId(google.get_id())
	global.SetStudentName(google.get_name())
	get_tree().change_scene("res://Presentation/CharSelect/CharSelect.tscn")

