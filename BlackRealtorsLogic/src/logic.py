import numpy as np
from src.prob import mult_prob, sum_prob, mult_prob_i, heat_map_coord
import matplotlib.pyplot as plt
from sklearn.preprocessing import normalize

DATA_SHAPE = (200, 200)
BORDER1 = (53.610826, 23.777779)
BORDER2 = (53.718562, 23.864186)
HEATMAP_SHAPE = (30, 30)
RADIUS = 5


class ScoreMemo:
    precalc = dict()
    sig = [25, 15, 5, 0]
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
            category = str(categoryDict['organizationType'])
            detailed_orgs = categoryDict['organizations']
            if detailed_orgs is None:
                continue

            orgs = [coord.to_idx(org['coordinates']['longitude'], org['coordinates']['latitude']) for org in detailed_orgs]
            for sig_id in range(0, 4):
                ScoreMemo.get_prob_by_category_sig(category, sig_id, orgs)
            
    probs_i = list()
    for categoryDict in all_orgs:
        category = str(categoryDict['organizationType'])
        detailed_orgs = categoryDict['organizations']
        sig_id = categoryDict['importanceLevel']

        for org in detailed_orgs:
            print(coord.to_idx(org['coordinates']['longitude'], org['coordinates']['latitude']))
        orgs = [coord.to_idx(org['coordinates']['longitude'], org['coordinates']['latitude']) for org in detailed_orgs]
        
        probs_i.append(ScoreMemo.get_prob_by_category_sig(category, sig_id, orgs))

    heatmap = sum_prob( mult_prob_i(probs_i), RADIUS)

    heatmap = normalize(heatmap, norm='l2')

    plt.imshow( heatmap )
    plt.show()



    # print(heatmap.shape)
    # return heatmap

    result = list()
    for x in np.linspace(BORDER1[0], BORDER2[0], HEATMAP_SHAPE[0]):
        for y in np.linspace(BORDER1[1], BORDER2[1], HEATMAP_SHAPE[1]):
            i, j = coord.to_idx(y, x)
            print(heatmap[i, j], i, j)
            result.append((x, y, np.power(heatmap[i, j], 0.3)))
    return result