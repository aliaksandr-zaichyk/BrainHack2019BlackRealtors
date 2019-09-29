import matplotlib.pyplot as plt
import numpy as np
import scipy.ndimage.filters as fi

def prob(shape, pos, nsig):
    universe = np.zeros( shape, dtype=float )
    x, y = pos
    universe[x, y] = 1
    return fi.gaussian_filter( universe, nsig )

def mult_prob(shape, positions, nsigs):
    universe = np.ones( shape, dtype=float )
    result = np.ones( shape, dtype=float )
    
    if np.size( nsigs ) == 1:
        x = nsigs
        nsigs = np.zeros( len( positions ), dtype=float)
        nsigs[:] = x
        
    for pos, nsig in zip( positions, nsigs ):
        result *= universe - prob( shape, pos, nsig )
    
    return result

def mult_prob_i(probs):
    result = np.ones( probs[0].shape, dtype=float )
    for p in probs:
        result *= p
    return np.ones( probs[0].shape, dtype=float ) - result

def sum_prob(p, r):
    N, M = p.shape    
    result = np.zeros( (N, M), dtype=float )
    tmp = np.zeros( (N+r*2, M+r*2), dtype=float )
    
    tmp[r:N+r, r:M+r] = p;
    
    k = np.zeros( (r*2+1, r*2+1), dtype=float )
    k[r, r] = 1.0
    k = fi.gaussian_filter( k, r/2 )
    
    for x in range(0, r*2+1):
        for y in range(0, r*2+1):
            if np.hypot( x-r, y-r ) > r:
                k[x, y] = 0.0
    
    for i in range(r, N+r):
        for j in range(r, M+r):
            result[i-r, j-r] = ( tmp[i-r:i+r+1, j-r:j+r+1] * k ).sum()
            
    return result

class heat_map_coord:
    def __init__ (self, shape, topleft, bottomright):
        self.shape = shape
        
        w, h = self.shape
        x1, y1 = topleft
        x2, y2 = bottomright
        
        if x1 > x2: x1, x2 = x2, x1 
        if y1 > y2: y1, y2 = y2, y1
        
        self.topleft = x1, y1
        self.bottomright = x2, y2
        
        self.x_scale = w / (x2 - x1) 
        self.y_scale = h / (y2 - y1)
    
    def to_idx(self, x, y):
        dx, dy = y - self.topleft[0], x - self.topleft[1]
        i, j = np.round( dx*self.x_scale ) , np.round( dy*self.y_scale )
        i = int( np.clip( i, 0, self.shape[0]-1 ) )
        j = int( np.clip( j, 0, self.shape[1]-1 ) )
        
        # print(x, y)
        # print(i, j)
        # print()
        # print(self.topleft[0], self.topleft[1])
        # # print(x, y)
        # exit(0)

        return i, j

# org1 = mult_prob( (200, 200), [(50, 50), (100, 150), (199, 0)], 5 )
# org2 = mult_prob( (200, 200), [(100, 100)], 7 )


# plt.imshow( mult_prob_i([org1, org2]) )
# plt.show()

# plt.imshow( sum_prob( mult_prob_i([org1, org2]), 70 ) )
# plt.show()
