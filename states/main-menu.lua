--[[
Main menu
]]--
states.main_menu = state_machine.new()

function states.main_menu:draw()
    love.graphics.print("Main Menu", 0, 0)
end

function states.splash:keypressed(key, code)
    if code == "space" then state_machine.switch(states.game) end
end
