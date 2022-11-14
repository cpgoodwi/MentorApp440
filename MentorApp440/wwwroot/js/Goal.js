
class Goal {
    constructor(goalStr, isComplete) {
        this.goalStr = goalStr
        this.isComplete = isComplete
    }
    
    toHTML() {
        return `<li>GOAL</li>`
    }
}
