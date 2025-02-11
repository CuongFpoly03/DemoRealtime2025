import * as SignalR from "@microsoft/signalr";

const userId = "6f9a7c12-4a33-47a7-982d-34e55eae3b19";
const connection = new SignalR.HubConnectionBuilder()
  .withUrl("http://localhost:5023/todoHub")
  .build();

connection
  .start()
  .then(() => {
    console.log("Connected to SignalR");
    connection.invoke("JoinGroup", userId);
  })
  .catch((err) => console.error("SignalR Connection Error:", err));

connection.on("ReceiveNotification", (message) => {
  alert("Thông báo: " + message);
});

export default connection;
