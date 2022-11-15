// console.log("Goal loaded")

class Goal {
    constructor(memId, goalId, goalStr, isComplete) {
        this.memId = memId
        this.goalId = goalId
        this.goalStr = goalStr
        this.isComplete = isComplete
    }
    
    toHTML() {
        return `<li>GOAL</li>`
    }
}
