from flask import Flask, request, jsonify
import datetime

app = Flask(__name__)

def write_log(entry: str):
    timestamp = datetime.datetime.utcnow().strftime("%Y-%m-%d %H:%M:%S UTC")
    with open("infractions.log", "a") as f:
        f.write(f"[{timestamp}] {entry}\n")

@app.route("/api/logs", methods=["POST"])
def receive_log():
    # Simple API key check
    api_key = request.headers.get("X-API-KEY")
    if api_key != "#h}=))r7az6P1Q5:^LK6]b5.*.c~jY.3":
        return jsonify({"error": "Unauthorized"}), 401

    data = request.get_json()
    log_entry = data["log"]
    write_log(log_entry)
    print(f"Log received: {log_entry}")

    return jsonify({"status": "success"}), 200

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000)
