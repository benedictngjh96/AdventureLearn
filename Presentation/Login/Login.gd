extends Node
var google = load("res://BusinessLogic/Google.gd").new()
var facebook= load("res://BusinessLogic/Facebook.gd").new()
#var login = load("res://BusinessLogic/LoginBL.cs").new()
var global = preload("res://Global/Global.cs");

func _ready():
    facebook.facebook_connect()
    #facebook.logout()
    #if facebook.isLoggedIn():
    #	facebook.logout()


func _on_LoginBtn_pressed():
    google.google_connect()
    global.SetGoogleId(google.get_id())
    global.SetStudentName(google.get_name())
    printNmae()
    #login.InsertUser()
    #google.achievement_unlock("CgkInLbspe0YEAIQAw")
    #google.achievement_show()
    #print(google.get_id())
    #print(google.get_name())
    #print(fb.getCurrentProfile())
    #print(fb.isLoggedIn())
    #print(fb.isLoggedIn())
    #get_tree().change_scene("res://Presentation/LevelSelection/LevelSelection.tscn")


func printNmae():
    print(facebook.getName())
    print(facebook.getId())
    get_node("Label").text = facebook.getName()
    
func _on_Button_pressed():
    facebook.login()
    printNmae()
    #print("da")
    #emit_signal("fb")
    #printNmae()


func _on_Button2_pressed():
    printNmae()
