// console.log("Goal loaded")

class Goal {
    constructor(memId, goalId, goalStr, isComplete) {
        this.memId = memId
        this.goalId = goalId
        this.goalStr = goalStr
        this.isComplete = isComplete
    }
    
    toHTML() {
        return (`
            <li>
                <input type="checkbox" id="goalcheck-${this.goalId}" ${this.isComplete ? 'checked' : ''} onchange="Goal.check(${this.memId},${this.goalId})" />
                <label for="goalcheck-${this.goalId}">${this.goalStr}</label>
            </li>
        `)
    }
    
    static check(memId, goalId) {
        // change the status of the item in the server
        console.log("goal status", memId, goalId)
    }
}
