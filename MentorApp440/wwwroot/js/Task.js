// console.log("Task Loaded")

class Task {
    constructor(memId, mentorName, mentorUsername, taskId, taskStr, isComplete) {
        this.memId = memId
        this.mentorName = mentorName
        this.mentorUsername = mentorUsername
        this.taskId = taskId
        this.taskStr = taskStr
        this.isComplete = isComplete
    }

    toHTML() {
        return `<li>TASK</li>`
    }
}