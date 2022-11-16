// console.log("User loaded")

const goalListElem = document.getElementById("goalListId")
const taskListElem = document.getElementById("taskListId")

// let userGoalList = []
// const pushGoal = (goal) => { userGoalList.push(goal) }
// console.log(userGoalList)

// let userTaskList = []
// const pushTask = (goal) => { userTaskList.push(goal) }
// console.log(userTaskList)

// console.log("from user.js: ", userGoalList)
// console.log("from user.js: ", userTaskList)

function loadGoals(userGoalList) {
    for (const goal of userGoalList) {
        goalListElem.innerHTML += new Goal(goal.memId, goal.goalId, goal.goalStr, goal.isComplete).toHTML()
    }
}

function loadTasks(userTaskList) {
    for (const task of userTaskList) {
        taskListElem.innerHTML += new Task(task.memId, task.mentorName, task.mentorUsername, task.taskId, task.taskStr, task.isComplete).toHTML()
    }
}

function goalChange() {
    // console.log("goals changed")
}

function taskChange() {
    console.log("task changed")
}