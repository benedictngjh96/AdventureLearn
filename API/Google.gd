extends Node
var google_play
var fb
signal info2

func google_connect():
	if Engine.has_singleton("PlayGames"):
		google_play = Engine.get_singleton("PlayGames")
		google_play.google_init(get_instance_id())
func gconnect():
	google_play.connect()
func google_disconnect():   
	print("googledc")
	google_play.disconnect()
func get_email():
	return google_play.getEmail()
func get_id():
	return google_play.getId()
func get_name():
	return google_play.getName()
func check_status():
	return google_play.checkStatus()
func achievement_unlock(achivementId):
	google_play.achievementUnlock(achivementId)
func achievement_show():
	google_play.achievementShowList()
func _on_successful_sign_in():
	emit_signal("info2")
func _on_failed_sign_in():
	print("da")
