extends Node2D
onready var char_health = $CharHealth
onready var mob_health = $MobHealth
onready var update_tween = $Tween
onready var char_sprite = $Char
onready var mob_sprite = $Mob

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.
func _process(delta):
	if(char_health.value<=0):
		get_tree().change_scene("res://Scenes/World/LevelCompleted.tscn")
	elif(mob_health.value<=0):
		get_tree().change_scene("res://Scenes/World/LevelCompleted.tscn")
func update_char_health(dmg_taken):
	#HP minus dmg
	var current_hp = char_health.value - dmg_taken
	update_tween.interpolate_property(char_health, "value", char_health.value, current_hp, 0.4, Tween.TRANS_SINE, Tween.EASE_IN_OUT)
	update_tween.start()
	
func update_mob_health(dmg_taken):
	#HP minus dmg
	var current_hp = mob_health.value - dmg_taken
	
	update_tween.interpolate_property(mob_health, "value", mob_health.value, current_hp, 0.4, Tween.TRANS_SINE, Tween.EASE_IN_OUT)
	update_tween.start()

func char_attack():
	char_sprite.play("attack")
	mob_sprite.play("hurt")
	yield(get_tree().create_timer(0.6), "timeout")
	idle_animation()
func mob_attack():
	char_sprite.play("hurt")
	mob_sprite.play("attack")
	yield(get_tree().create_timer(0.6), "timeout")
	idle_animation()

func idle_animation():
	mob_sprite.play("idle")
	char_sprite.play("idle")

func correct_ans(dmg):
	char_attack()
	update_mob_health(dmg)

func wrong_ans(dmg):
	mob_attack()
	update_char_health(dmg)
func _on_Option1_pressed():
	correct_ans(25)


func _on_Option2_pressed():
	wrong_ans(25)


func _on_Option3_pressed():
	wrong_ans(25)


func _on_Option4_pressed():
	wrong_ans(25)
