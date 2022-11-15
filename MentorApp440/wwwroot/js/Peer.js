// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

class Peer {
    constructor(name) {
        this.name = name
    }

    renderHTML() {
        return (
            `<li>
                <ul>
                    <img src="~/lib/Images/Profile.jpeg" class="img-thumbnail" id="peerPic" alt="" width="10px"
                         height="10px"/>
                    <a href="">${this.name}</a>
                </ul>
            </li>`
        )
    }
}