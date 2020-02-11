extends Node2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

var play_games_services
# Called when the node enters the scene tree for the first time.
func _ready():
	
	if Engine.has_singleton("PlayGameServices"):
		play_games_services = Engine.get_singleton("PlayGameServices")
		
		var show_popups = true # For example, your game can display a “Welcome back” or an “Achievements unlocked” pop-up. true for enabling it.
		play_games_services.init(get_instance_id(), show_popups)
		#play_games_services.sign_in()
		#if(play_games_services):
		#	get_node("Label").set_text("on")
		#else:
		#	get_node("Label").set_text("failed")
		sign_in()
		if(is_player_connected()):
			get_node("Label").set_text("true")
		else:
			get_node("Label").set_text("false")
		#get_node("Label").set_text(is_player_connected())




# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_TextureButton_pressed():
	sign_out()
	sign_in()
	#get_tree().change_scene("res://Scenes/CharSelect/CharSelect.tscn")
	#get_tree().change_scene("res://Scenes/Fight/Fight.tscn")
	#sign_in()
	pass


# Sign-in/sign-out methods
func sign_in() -> void:
	if play_games_services:
		play_games_services.sign_in()


func sign_out() -> void:
	if play_games_services:
		play_games_services.sign_out()


# Connection methods
func is_player_connected() -> void:
	if play_games_services:
		play_games_services.is_player_connected()


# Achievements methods
func unlock_achievement() -> void:
	if play_games_services:
		play_games_services.unlock_achievement("ACHIEVEMENT_ID")


func reveal_achievement() -> void:
	if play_games_services:
		play_games_services.reveal_achievement("ACHIEVEMENT_ID")


func increment_achievement() -> void:
	if play_games_services:
		var step = 1 
		play_games_services.increment_achievement("ACHIEVEMENT_ID", step)


func show_achievements() -> void:
	if play_games_services:
		play_games_services.show_achievements()


# Leaderboards methods
func show_leaderboard() -> void:
	if play_games_services:
		play_games_services.show_leaderboard("LEADERBOARD_ID")


func submit_leaderboard_score() -> void:
	if play_games_services:
		var score = 1234
		play_games_services.submit_leaderboard_score("LEADERBOARD_ID", score)


# Callbacks
# Sign-in / sign-out callbacks
func _on_sign_in_success() -> void:
	pass

func _on_sign_in_failed(error_code: int) -> void:
	pass

func _on_sign_out_success() -> void:
	pass

func _on_sign_out_failed() -> void:
	pass


# Connection callbacks
func _on_player_is_already_connected(is_connected: bool) -> void:
	pass


# Achievements callbacks
func _on_achievement_unlocked(achievement: String) -> void:
	pass

func _on_achievement_unlocking_failed(achievement: String) -> void:
	pass

func _on_achievement_revealed(achievement: String) -> void:
	pass

func _on_achievement_revealing_failed(achievement: String) -> void:
	pass

func _on_achievement_incremented(achievement: String) -> void:
	pass

func _on_achievement_incrementing_failed(achievement: String) -> void:
	pass


# Leaderboards callbacks
func _on_leaderboard_score_submitted(leaderboard_id: String) -> void:
	pass

func _on_leaderboard_score_submitting_failed(leaderboard_id: String) -> void:
	pass
