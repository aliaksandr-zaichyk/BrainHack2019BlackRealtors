import numpy as np
from src.prob import mult_prob, sum_prob, mult_prob_i, heat_map_coord
import matplotlib.pyplot as plt

DATA_SHAPE = (200, 200)
BORDER1 = (53.607187, 23.702459)
BORDER2 = (53.759638, 24.019003)
HEATMAP_SHAPE = (100, 100)
RADIUS = 40


class ScoreMemo:
    precalc = dict()
    sig = [0, 5, 7, 25]
    mapping = dict()


    @staticmethod
    def get_prob_by_category_sig(categoty, sig_id, orgs):
        if (categoty, sig_id) not in ScoreMemo.precalc:


            ScoreMemo.precalc[(categoty, sig_id)] = mult_prob(
                DATA_SHAPE,
                orgs,
                ScoreMemo.sig[sig_id]
                )
        return ScoreMemo.precalc[(categoty, sig_id)]
    
    @staticmethod
    def is_empty():
        return len(ScoreMemo.precalc) == 0
        

def getScore(all_orgs):
    coord = heat_map_coord(DATA_SHAPE, BORDER1, BORDER2)

    if ScoreMemo.is_empty():
        for categoryDict in all_orgs:
            category = categoryDict['organizationType']
            detailed_orgs = categoryDict['organizations']
            

            orgs = [coord.to_idx(org['coordinates']['longitude'], org['coordinates']['latitude']) for org in detailed_orgs]
            for sig_id in range(0, 4):
                ScoreMemo.get_prob_by_category_sig(category, sig_id, orgs)
            
    probs_i = list()
    for categoryDict in all_orgs:
        category = categoryDict['organizationType']
        detailed_orgs = categoryDict['organizations']
        sig_id = categoryDict['importanceLevel']

        orgs = [coord.to_idx(org['coordinates']['longitude'], org['coordinates']['latitude']) for org in detailed_orgs]
        
        probs_i.append(ScoreMemo.get_prob_by_category_sig(category, sig_id, orgs))

    heatmap = sum_prob( mult_prob_i(probs_i), RADIUS)
    # print(heatmap.shape)
    # return heatmap

    result = list()
    for x in np.linspace(BORDER1[0], BORDER2[0], HEATMAP_SHAPE[0]):
        for y in np.linspace(BORDER1[1], BORDER2[1], HEATMAP_SHAPE[1]):
            i, j = coord.to_idx(x, y)
            result.append((x, y, heatmap[i, j]))
    return result

ёбаный_блять_json = [
    {
        "organizationType":  "kachalka",
        "importanceLevel": 1,
        "organizations": [
        {
            "coordinateX" : 53.709949,
            "coordinateY": 23.809282,
        },
        {
            "coordinateX" : 53.624271,
            "coordinateY": 23.806381,
        },
        ]
    },
    # {
    #     "organizationType":  "sharaga",
    #     "ImportanceLevel": 2,
    #     "organizations": [
    #     {
    #         "coordinateX" : 200,
    #         "coordinateY": 200,
    #     },
    #     {
    #         "coordinateX" : 100,
    #         "coordinateY": 100,
    #     },
    #     ]
    # }
]

# plt.imshow(getScore(ёбаный_блять_json))
# plt.show()


def logic(data):
    return [
        (1, 2, 0.1),
        (2, 3, 0.1488),
        (2, 3, 0.1337),
        ]
    # return np.zeros(shape=(100, 100))