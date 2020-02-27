extends Node
var fb
var count = 0
func facebook_connect():
    if Engine.has_singleton("GodotFacebook"):
        fb = Engine.get_singleton("GodotFacebook")
        fb.init("1530305797147294")
        fb.setFacebookCallbackId(get_instance_id())

func getName():
    return fb.getName()
func getId():
    return fb.getId()
    
func isLoggedIn():
    return fb.isLoggedIn()
func printName():
    print(fb.getName())
func login():
    fb.login()
    print("login")

func logout():
    fb.logout()
    print("loggedout")
