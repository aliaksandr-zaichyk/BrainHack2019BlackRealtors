import numpy as np
from src.prob import mult_prob, sum_prob, mult_prob_i, heat_map_coord
import matplotlib.pyplot as plt
from sklearn.preprocessing import normalize
from PIL import Image

DATA_SHAPE = (200, 200)
BORDER1 = (53.610826, 23.777779)
BORDER2 = (53.718562, 23.864186)
HEATMAP_SHAPE = (30, 30)
RADIUS = 5
IMG_PATH = "../BlackRealtors/BlackRealtors/wwwroot/images/final_map.png"

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
            for sig_id in range(1, 4):
                ScoreMemo.get_prob_by_category_sig(category, sig_id, orgs)
            
    probs_i = list()
    for categoryDict in all_orgs:
        category = str(categoryDict['organizationType'])
        detailed_orgs = categoryDict['organizations']
        sig_id = categoryDict['importanceLevel']
        if sig_id == 0:
            print(100500)
            continue

        for org in detailed_orgs:
            print(coord.to_idx(org['coordinates']['longitude'], org['coordinates']['latitude']))
        print(len(detailed_orgs))
        orgs = [coord.to_idx(org['coordinates']['longitude'], org['coordinates']['latitude']) for org in detailed_orgs]
        
        probs_i.append(ScoreMemo.get_prob_by_category_sig(category, sig_id, orgs))

    heatmap = sum_prob( mult_prob_i(probs_i), RADIUS)

    plt.axis('off')
    plt.imshow( heatmap, cmap='Greys', interpolation='spline36' )
    plt.gca().invert_yaxis()
    plt.savefig('map.png', bbox_inches='tight', pad_inches=0)

    img = Image.open('map.png')
    img = img.convert("RGBA")
    datas = img.getdata()


    plt.imshow( heatmap )
    plt.show()

    newData = []
    for item in datas:
        avg = (item[0] + item[1] + item[2]) // 3
        newData.append((0, 255, 0, 255-avg))
    
    img.putdata(newData)
    img.save(IMG_PATH, "PNG")



    # print(heatmap.shape)
    # return heatmap

    result = list()
    for x in np.linspace(BORDER1[0], BORDER2[0], HEATMAP_SHAPE[0]):
        for y in np.linspace(BORDER1[1], BORDER2[1], HEATMAP_SHAPE[1]):
            i, j = coord.to_idx(y, x)
            # print(heatmap[i, j], i, j)
            result.append((x, y, np.power(heatmap[i, j], 0.3)))
    return result