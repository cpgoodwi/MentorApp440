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
        return (`
            <li>
                <input type="checkbox" id="goalcheck-${this.taskId}" ${this.isComplete ? 'checked' : ''} onchange="Task.check(${this.memId},${this.taskId})" />
                <label for="goalcheck-${this.taskId}">${this.taskStr}</label>
            </li>
        `)
    }
    
    static check(memId, taskId) {
        // change the status of this item in the server
        console.log("task status", memId, taskId)
    }
}