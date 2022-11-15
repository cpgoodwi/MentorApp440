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
    for (const goal of userTaskList) {
        taskListElem.innerHTML += new Task(goal.memId, goal.goalId, goal.goalStr, goal.isComplete).toHTML()
    }
}

function goalChange() {
    // console.log("goals changed")
}

function taskChange() {
    console.log("task changed")
}