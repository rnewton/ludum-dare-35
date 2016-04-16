Player = class('Player', Entity)

function Player:start()
    self:initPosition(0, 0)
    self:initRotation(0)
    self:initScale(1, 1)
    self:initSprite("assets/coin.png")
end

function Player:update(dt)
    self.position = vector(love.mouse.getX(), love.mouse.getY())
end

function Player:draw()
    if self.sprite ~= nil then
        self:drawSprite()
    end
end

-- Components
Player:include(Position)
Player:include(Rotation)
Player:include(Scale)
Player:include(Sprite)

-- Add to entity pool
emma.addEntity('Player', {'players'})
