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

function loadGoals(userGoalsList) {
    for (const goal of userGoalsList) {
        goalListElem.innerHTML += new Goal(goal.memId, goal.goalId, goal.goalStr, goal.isComplete).toHTML()
    }
}


