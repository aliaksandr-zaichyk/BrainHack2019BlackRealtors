from src.logic import getScore
from flask import Flask, request
import json

app = Flask(__name__)

@app.route("/pscore", methods=['POST'])
def searcher_from_post():
    data = json.loads(request.data)
    
    hots = getScore(data)

    return json.dumps([{'coordinates': {'longitude':x, 'latitude':y}, 'weight':hot} for x, y, hot in hots])
    

if __name__ == '__main__':
    app.run(host='127.0.0.1', port=5002)