extends Control
onready var chart_node = get_node('chart')

var stats = load("res://BusinessLogic/StatisticsBL.cs")
var statsBL = stats.new()
onready var worldOption = get_node("TabContainer/Campaign/menu/world")
onready var sectionOption = get_node("TabContainer/Campaign/menu/section")
onready var studentOption = get_node("student")
onready var assignmentOption = get_node("TabContainer/Assignment/assignment")
onready var nameLbl = get_node("TabContainer/Assignment/Name")
onready var studentNumLbl = get_node("TabContainer/Assignment/StudentNum")
onready var maxScore = get_node("TabContainer/Assignment/Max")
onready var minScore = get_node("TabContainer/Assignment/Min")
onready var avgScore = get_node("TabContainer/Assignment/Avg")
onready var title = get_node("TabContainer/Campaign/menu/Title")
var assignment_list = statsBL.GetAssignments()
var world_list = statsBL.GetWorldSections()
var section_list;
var student_list  = statsBL.GetStudents() 
var worldId
var sectionId
var studentId
var assignmentId
var w = 0 
func _ready():
  set_process(true)
  
  for item in assignment_list:
    assignmentOption.add_item(item.AssignmentName)
  assignmentId = assignment_list[0].AssignmentId
  for item in world_list:
    worldOption.add_item(item.WorldName)
  for sect in world_list[0].Section:
    sectionOption.add_item(sect.SectionName)
  for item in student_list:
    studentOption.add_item(item.StudentName)


func pie_chart():
  chart_node.initialize(chart_node.LABELS_TO_SHOW.NO_LABEL,
  {
    star1 = Color(0.58, 0.92, 0.07),
    star2 = Color(1.0, 0.18, 0.18),
    star3 = Color(0.5, 0.22, 0.6)
  })
  chart_node.create_new_point({
    label = 'JANVIER',
    values = {
      star1 = 60,
      star2 = 35,
      star3 = 10
    }
  })
func load_chart():
    chart_node.clear_chart()
    chart_node.initialize(chart_node.LABELS_TO_SHOW.LEGEND_LABEL,
    {
        score = Color(0.58, 0.92, 0.07)
    })
func set_assignment():
    for assignment in assignment_list:
        if assignment.AssignmentName == assignmentOption.text:
            assignmentId = assignment.AssignmentId
func set_world():
   for world in world_list:
        if world.WorldName == worldOption.text:
            worldId = world.WorldId
func set_section():
   for section in world_list[w].Section:
        if section.SectionName == sectionOption.text:
            sectionId  = section.SectionId
func set_student():
   for student in student_list:
        if student.StudentName == studentOption.text:
            studentId = student.StudentId  
func _on_StudentBtn_pressed():
    var count = 1
    load_chart()
    set_world()
    set_section()
    set_student()
  
    var student_scores = statsBL.GetStudentScores(worldId,sectionId,studentId)
    if student_scores != null:
      for item in student_scores:
        chart_node.create_new_point({
            label = "Lv"+str(count),
            values = {
                score = item.LevelScore
               }
           })
        count= count+1

func _on_AvgSectionBtn_pressed():
    var count =1
    load_chart()
    set_world()
    var avgScores = statsBL.GetAvgSectionScores(worldId)
    if avgScores != null:
        for item in avgScores: 
          chart_node.create_new_point({
            label = "S"+str(count),
            values = {
                score = item.LevelScore
               }
           })
          count= count+1              
    title.text = worldOption.text + " " + sectionOption.text
    

    
func _on_AvgWorldBtn_pressed():
    var count = 1
    load_chart()
    var avgScores = statsBL.GetAvgWorldScores()
    if avgScores != null:
        for item in avgScores: 
          chart_node.create_new_point({
            label = "W"+str(count),
            values = {
                score = item.LevelScore
               }
           })
          count= count+1     
    title.text = worldOption.text
func _on_world_item_selected(id):
    sectionOption.clear()
    w = id
    for item in world_list[id].Section:
        sectionOption.add_item(item.SectionName)


func _on_AssignmentBtn_pressed():
    var count = 1
    load_chart()
    set_assignment()
 
    var avgScores = statsBL.GetAvgAssignmentScore()
    var maxS = statsBL.GetMaxAssignmentScore(assignmentId)
    var minS = statsBL.GetMinAssignmentScore(assignmentId)
    var avgS = statsBL.GetAvgAssignmentScore(assignmentId)
    if avgScores != null:
        for item in avgScores: 
          chart_node.create_new_point({
            label = "As"+str(count),
            values = {
                score = item.Score
               }
           })
          count= count+1    
    nameLbl.text = assignmentOption.text
    studentNumLbl.text = "Number of students:" + str(count)
    maxScore.text = "HighestScore:" +str(maxS.Score)
    minScore.text = "LowestScore:" +str(minS.Score)
    avgScore.text = "AverageScore:" +str(avgS.Score)

func _on_StudentAssignmentBtn_pressed():
    var count = 1
    load_chart()
    set_student()
    var student_scores = statsBL.GetStudentAssignmentScores(studentId)
    if student_scores != null:
      for item in student_scores:
        chart_node.create_new_point({
            label = "As"+str(count),
            values = {
                score = item.Score
               }
           })
        count= count+1
    
