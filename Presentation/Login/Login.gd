extends Node
var google = load("res://API/Google.gd").new()
var facebook= load("res://API/Facebook.gd").new()
var student = load("res://BusinessLogic/StudentBL.cs")
var studentBL = student.new()
var global = preload("res://Global/Global.cs");
var email
var googleId
var fbId

func _ready():
    facebook.facebook_connect()
    facebook.connect("info", self, "insert_fb")
    google.connect("info2", self, "insert_google")
    #google.google_disconnect()    
    var exist = facebook.isLoggedIn()
    if exist:
        facebook.logout()

func _on_LoginBtn_pressed():
    google.google_connect()
    google.gconnect()
func insert_google():
    var exist = studentBL.CheckGoogleExist(google.get_id())
    if exist:
        global.SetStudentId(studentBL.GetGoogleStudentId(google.get_id()))
        if studentBL.CheckGoogleCharExist(google.get_id()):
            get_tree().change_scene("res://Presentation/MainMenu/MainMenu.tscn")
        else:
            get_tree().change_scene("res://Presentation/CharSelect/CharSelect.tscn")
    else:
        studentBL.InsertGoogleStudent(google.get_name(), google.get_email(), google.get_id())
        global.SetStudentId(studentBL.GetGoogleStudentId(google.get_id()))
        get_tree().change_scene("res://Presentation/CharSelect/CharSelect.tscn")
func insert_fb():
    email = facebook.getEmail()
    var exist = studentBL.CheckFacebookExist(facebook.getId())
    if exist:
        global.SetStudentId(studentBL.GetFacebookStudentId(facebook.getId()))
        if studentBL.CheckFacebookCharExist(facebook.getId()):
            get_tree().change_scene("res://Presentation/MainMenu/MainMenu.tscn")
        else:
            get_tree().change_scene("res://Presentation/CharSelect/CharSelect.tscn")
    else:
        studentBL.InsertFacebookStudent(facebook.getName(), email, facebook.getId())
        global.SetStudentId(studentBL.GetFacebookStudentId(facebook.getId()))
        get_tree().change_scene("res://Presentation/CharSelect/CharSelect.tscn")
        
func _on_FbLogin_pressed():
    facebook.login()
