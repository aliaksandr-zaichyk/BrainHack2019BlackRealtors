from src.logic import logic
from flask import Flask, request
import json

app = Flask(__name__)

@app.route("/pizdatiy_score", methods=['POST'])
def searcher_from_post():
    data = request.data

    print(data)
    hots = logic(data)
    # hots = [{'coordinates': {'longitude':500, 'latitude':100}, 'weight':0.1488}]
    # return json.dumps(hots)

    return 'CXC'
    # return json.dumps([{'coordinates': {'longitude':x, 'latitude':y}, 'weight':hot} for x, y, hot in hots])
    

if __name__ == '__main__':
    app.run(host='127.0.0.1', port=5000)
