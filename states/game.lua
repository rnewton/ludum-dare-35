--[[
Game
]]--
states.game = state_machine.new()

function states.game:enter()
    player = emma.instantiate('Player')
end

function states.game:update(dt)
    emma.update(dt)
end

function states.game:draw()
    love.graphics.print("Game", 0, 0)
    emma.draw()
end
