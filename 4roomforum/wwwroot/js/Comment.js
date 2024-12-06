

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/comment")  
    .build();

// Bắt đầu kết nối SignalR
connection.start().catch(function (err) {
    return console.error(err.toString());
});

// Lắng nghe sự kiện 'ReceiveComment' từ server
connection.on("ReceiveComment", async function (postId, replyContent, userName, replyToReply, replyId) {
    // Lấy thời gian hiện tại cho bình luận
    var currentTime = new Date().toLocaleString();
    
    let replyTo = '';
    let user = '';
    console.log(replyId);
    if (replyToReply != null) {
        
        replyTo = await getReplyById(replyToReply);

        user = await getUserNameById(replyTo.repliedBy)

    }
            
    // Định nghĩa cấu trúc cho bình luận
    var commentHtml = `
        <div class="row mb-4">
            <div class="col-md-2 text-center">
                <div class="bg-warning text-dark rounded-circle d-flex justify-content-center align-items-center mx-auto" style="width: 60px; height: 60px;">
                    <strong>KCC</strong> 
                </div>
                <p class="mt-2 fw-bold">${userName}</p>
            </div>
            <div class="col-md-10">
                <div class="card">
                    <div class="card-header bg-light">
                        <strong>${currentTime}</strong>
                    </div>
                     <div class="card-body">
                        ${replyToReply ? `
                            
                            <blockquote class="p-2 bg-light border rounded">
                                <p>
                                    <strong class="text-primary form-label">${user.userName} said :</strong>
                                    <br>
                                    <em>${replyTo.replyContent}</em> 
                                </p>
                            </blockquote>
                            <p class="mt-3">${replyContent}</p>
                        ` : `
                            <p class="mt-3">${replyContent}</p>
                        `}
                        
                    </div>
                    <div class="card-footer">
                        <div class="d-flex flex-row justify-content-start">
                            <div class="p-2">
                                <i class="fa-solid fa-thumbs-up"></i>
                                <span>10</span> 
                            </div>
                            <div class="p-2">
                                <i class="fa-regular fa-thumbs-down"></i>
                                <span>-10</span>
                            </div>
                            <div class="p-2">
                                <button class="comment-for-reply" value='["${postId}", "${userName}", "${replyId}"]'>
                                    <i class="fa-solid fa-comment"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `;

    // Thêm bình luận mới vào container
    document.getElementById("commentsContainer").insertAdjacentHTML('afterbegin', commentHtml);
    addReplyEventListeners();
});
function addReplyEventListeners() {
    var elements = document.querySelectorAll(".comment-for-reply");

    elements.forEach(function (element) {
        element.addEventListener('click', function () { 
        
            var value = JSON.parse(this.value);
            
         
            document.querySelector('.sticky-form').style.display = 'block';
   
            document.querySelector('.form-label1').innerHTML = "You're replying to " + value[1] + "'s Comment";

            //var replyID = getReplyId(value[0]);
            //console.log(replyID);
            document.getElementById('replyToReply').value = value[2];
        });
    });
}
//async function getReplyId(postId) {
//    try {
//        const response = await fetch(`/Reply/GetReplyIdNew?PostId=${postId}`, {
//            method: "GET",
//            headers: {
//                "Content-Type": "application/json"
//            }
//        });

//        if (!response.ok) {
//            throw new Error("Failed to fetch the latest reply ID.");
//        }

//        const result = await response.json();

//        if (result.replyId) {
//            console.log("Latest Reply ID:", result.replyId);
//        } else {
//            console.log("No reply found.");
//        }

//        return result.ReplyId;
//    } catch (error) {
//        console.log("Error:", error.message);
//    }
//}
async function getReplyById(replyId) {
    try {
        const response = await fetch(`Reply/GetReplyById?ReplyId=${replyId}`);
        if (!response.ok) {
            console.error("Not success");
        }
        const data = await response.json();
        
        return data;
    } catch(error) {
        console.log(error); 
    }
}
async function getUserNameById(userId) {
    try {
        const response = await fetch(`/Reply/GetNameUserById?UserId=${userId}`);
        if (!response.ok) {
            console.error("Not success");
        }
        const data = await response.json();
  
        return data;
    } catch (error) {
        console.log(error);
    }
}






// Gửi bình luận khi form được submit
document.getElementById("replyForm").addEventListener("submit", function (event) {
    event.preventDefault();
   
    fetch("/Reply/Create", {
        method: "POST",
        body: new FormData(document.getElementById("replyForm"))
    }).then(function (response) {
        if (response.ok) {
            document.getElementById("replyForm").reset();
            document.querySelector('.sticky-form').style.display = 'none';
        }
       
    });
});
