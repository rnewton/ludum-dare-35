Sprite = {
    initSprite = function(self, image)
        assert(type(self.hasPosition) == "function", "This entity must have a position.")
        assert(type(self.hasRotation) == "function", "This entity must have a rotation.")
        assert(type(self.hasScale) == "function", "This entity must have a scale.")

        self.sprite = love.graphics.newImage(image)
    end,

    drawSprite = function(self)
        love.graphics.draw(
            self.sprite,
            self.position.x,
            self.position.y,
            self.rotation,
            self.scale.x,
            self.scale.y
        )
    end,

    isDrawable = function(self)
        return true
    end
}
